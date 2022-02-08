namespace MacCatApp

open System
open Foundation
open CoreGraphics
open ObjCRuntime
open UIKit

type SidebarViewController() =
    inherit UIViewController()

    let layout = new UICollectionViewCompositionalLayout(UICollectionViewCompositionalLayoutSectionProvider (fun sectionIndex env ->
        let config = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.Sidebar)
        config.ShowsSeparators <- false
        //config.HeaderMode <- UICollectionLayoutListHeaderMode.FirstItemInSection
        let section = NSCollectionLayoutSection.GetSection(config, env)
        section))

    let collectionView = new UICollectionView (new CGRect(0f, 0f, 100f, 100f), layout)

    let importImage = UIImage.GetSystemImage "cube"

    let rowConfig = UICollectionViewCellRegistration.GetRegistration(typeof<UICollectionViewListCell>, fun cell indexPath item ->
        //printfn "CONFIG CELL ITEM: %O" item
        let config = UIListContentConfiguration.SidebarSubtitleCellConfiguration
        match item with
        | x ->
            config.Text <- string x
            config.Image <- importImage
        cell.ContentConfiguration <- config
        ())

    let mutable items : SidebarItem[] = Array.empty

    let mutable vsubs : IDisposable[] = Array.empty

    override this.LoadView () =
        this.View <- collectionView

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        collectionView.Frame <- this.View.Bounds
        collectionView.AutoresizingMask <- UIViewAutoresizing.FlexibleDimensions
        collectionView.DataSource <- this
        collectionView.AllowsMultipleSelection <- true
        collectionView.SelectionFollowsFocus <- true
        collectionView.Delegate <- new SidebarDelegate ()
        this.Title <- "Sidebar"

        vsubs <-
            [|
            |]
        this.NavigationItem.LeftBarButtonItems <-
            [|
            |]
        this.NavigationItem.RightBarButtonItems <-
            [|
            |]

    interface IUICollectionViewDataSource with
        member this.GetItemsCount (collectionView, section) =
            nint items.Length
        member this.GetCell (collectionView, indexPath) =
            let item = items.[indexPath.Row]
            collectionView.DequeueConfiguredReusableCell(rowConfig, indexPath, item)

    member this.RefreshItems () =
        items <- [||]
        collectionView.ReloadData ()

    override this.SelectAll sender =
        for row, item in items |> Array.indexed do
            collectionView.SelectItem(NSIndexPath.FromRowSection(nint row, nint 0), true, UICollectionViewScrollPosition.None)
        ()

and SidebarItem (value : string) =
    inherit NSObject ()
    member this.Value = value
    override this.ToString () = string this.Value

and SidebarDelegate () =
    inherit UIKit.UICollectionViewDelegate ()
    override this.ItemSelected (collectionView, indexPath) =
        ()







