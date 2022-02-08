namespace MacCatApp

open System
open System.Threading.Tasks
open UIKit
open Foundation
open ObjCRuntime

type MainViewController () =
    inherit UISplitViewController(UISplitViewControllerStyle.DoubleColumn)

    member this.OpenUrls (urls : NSUrl[]) =
        ()

    member this.HandleAppActivities(activities : AppActivity[]) =
        if activities.Length = 0 then
            //let emptyVC = new EmptyProjectDialog (projectStore)
            //emptyVC.PresentAsSheet (this, fun x ->
            //    match x with
            //    | 2 -> this.Import(this)
            //    | _ -> ())
            ()
        else
            for a in activities do
                match a with
                | OpenActivity ->
                    let types = [|UniformTypeIdentifiers.UTType.CreateFromExtension("txt")|]
                    let picker = new UIDocumentPickerViewController (types, AllowsMultipleSelection=true)
                    let mutable sub : System.IDisposable option = None
                    sub <- Some (picker.DidPickDocumentAtUrls.Subscribe(fun e ->
                        let urls = e.Urls
                        this.OpenUrls urls
                        if sub.IsSome then sub.Value.Dispose()
                        sub <- None
                        ()))
                    this.PresentViewController(picker, true, null)

