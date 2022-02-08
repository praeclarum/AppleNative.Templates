namespace AppleNativeApp

open System
open Foundation
open ObjCRuntime
open UIKit
open CoreGraphics

module AppCommands =
    let selector (sel : string) = new Selector (sel)
    let command (title, sel : string) : UICommand =
        UICommand.Create(title, null, selector sel, null, Array.empty)
    let kcommand (title, sel : string, modifierFlags, input) : UIKeyCommand =
        UIKeyCommand.Create(title, null, selector sel, input, modifierFlags, null, Array.empty)

    let openDocumentSel = "open:"
    let openDocument = kcommand ("Open...", openDocumentSel, UIKeyModifierFlags.Command, "O")
    let saveDocument = kcommand ("Save...", "save:", UIKeyModifierFlags.Command, "S")


type AppActivity =
    | OpenActivity
