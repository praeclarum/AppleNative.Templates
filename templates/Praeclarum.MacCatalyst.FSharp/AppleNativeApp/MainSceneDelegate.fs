namespace AppleNativeApp

open System
open System.Threading.Tasks
open UIKit
open Foundation
open ObjCRuntime

[<Register ("MainSceneDelegate")>]
type MainSceneDelegate () =
    inherit UIWindowSceneDelegate ()

    let mutable detailViewController : DetailViewController option = None
    let mutable sidebarViewController : SidebarViewController option = None
    let split = new MainViewController ()

    let mutable loadedExamples = false

//-:cnd:noEmit
#if __MACCATALYST__
    let toolbarDelegate = new MainToolbarDelegate ()
#endif
//+:cnd:noEmit

    override val Window = null with get, set

    override this.WillConnect (scene, session, connectionOptions) =

        let activities = connectionOptions.UserActivities.ToArray()
        let tasks = ResizeArray<_>()
        for a in activities do
            printfn "ACT: %A" a.ActivityType
            match a.ActivityType with
            | "com.example.AppleNativeApp.open" ->
                tasks.Add(OpenActivity)
            | s ->
                printfn "UNKNOWN ACT: %s:" s

        match scene with
        | :? UIWindowScene as scene ->
            let vc = new DetailViewController ()
            let ic = new SidebarViewController ()
            detailViewController <- Some vc
            sidebarViewController <- Some ic
            let icn = new UINavigationController(ic)
            let vcn = new UINavigationController(vc)
            split.PrimaryBackgroundStyle <- UISplitViewControllerBackgroundStyle.Sidebar
            split.SetViewController(icn, UISplitViewControllerColumn.Primary)
            split.SetViewController(vcn, UISplitViewControllerColumn.Secondary)

//-:cnd:noEmit
#if __MACCATALYST__
            icn.NavigationBarHidden <- true
            vcn.NavigationBarHidden <- true
            let toolbar = new AppKit.NSToolbar(identifier = "main")
            toolbar.Delegate <- toolbarDelegate
            toolbar.DisplayMode <- AppKit.NSToolbarDisplayMode.Icon
            toolbar.CenteredItemIdentifier <- "com.example.AppleNativeApp.segments"
            toolbar.AllowsUserCustomization <- true
            match scene.Titlebar with
            | null -> ()
            | titlebar ->
                titlebar.Toolbar <- toolbar
                titlebar.ToolbarStyle <- UITitlebarToolbarStyle.Automatic
#endif
//+:cnd:noEmit

            let win = new UIWindow (scene.CoordinateSpace.Bounds)
            
            win.RootViewController <- split
            this.Window <- win
            win.WindowScene <- scene
            win.MakeKeyAndVisible()
            async {                
                do! Async.Sleep 100
                split.HandleAppActivities(tasks.ToArray())
            }
            |> Async.StartImmediate
        | _ -> ()

//-:cnd:noEmit
#if __MACCATALYST__

and MainToolbarDelegate () =
    inherit AppKit.NSToolbarDelegate ()

    let defaultItems =
        [|
            string AppKit.NSToolbar.NSToolbarToggleSidebarItemIdentifier
            "com.example.AppleNativeApp.segments"
            string AppKit.NSToolbar.NSToolbarFlexibleSpaceItemIdentifier
            "com.example.AppleNativeApp.open"
            string AppKit.NSToolbar.NSToolbarSpaceItemIdentifier
            "com.example.AppleNativeApp.menu"
        |]

    override this.DefaultItemIdentifiers (toolbar) =
        defaultItems

    override this.AllowedItemIdentifiers (toolbar) =
        [|
            yield! this.DefaultItemIdentifiers toolbar
        |]

    override this.WillInsertItem (toolbar, itemIdentifier, willBeInserted) =
        match itemIdentifier with
        | "com.example.AppleNativeApp.segments" ->
            let item = AppKit.NSToolbarItemGroup.Create(itemIdentifier, [|"X"; "Y";"Z"|], AppKit.NSToolbarItemGroupSelectionMode.SelectOne, [|"X";"Y"; "Z"|], null, new Selector("setAxis:"))
            item.SelectedIndex <- 0
            item.ToolTip <- "Choose one."
            upcast item
        | "com.example.AppleNativeApp.open" ->
            let item = new AppKit.NSToolbarItem(itemIdentifier, Label = "Open");
            item.Image <- AppKit.NSImage.GetSystemSymbol("cube", "Open")
            item.ToolTip <- "Open"
            item.Action <- new Selector(AppCommands.openDocumentSel)
            item
        | "com.example.AppleNativeApp.menu" ->
            let item = new AppKit.NSMenuToolbarItem ()
            let ident = new NSString (itemIdentifier)
            let res = item.PerformSelector(new ObjCRuntime.Selector("initWithItemIdentifier:"), ident)
            item.Label <- "Menu"
            let image = AppKit.NSImage.GetSystemSymbol("cube", "Menu")
            item.Image <- image
            item.ToolTip <- "Show a menu."
            item.ItemMenu <- UIMenu.Create([||])
            item.ShowsIndicator <- true
            upcast item
        | s ->
            printfn "Bad toolbar item: %s" s
            null

#endif
//+:cnd:noEmit
