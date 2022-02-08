namespace MacCatApp

open System
open System.Threading.Tasks
open UIKit
open Foundation
open ObjCRuntime

type DetailViewController () =
    inherit UIViewController()

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        this.View.BackgroundColor <- UIColor.SystemBackgroundColor
