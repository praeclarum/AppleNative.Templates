namespace AppleNativeApp

open Foundation
open ObjCRuntime
open UIKit

[<Register("AppDelegate")>]
type AppDelegate() =
    inherit UIResponder()

    let mutable window = None

    interface IUIApplicationDelegate

    [<Export("application:didFinishLaunchingWithOptions:")>]
    member this.FinishedLaunching(application : UIApplication, launchOptions : NSDictionary) : bool =
        true

    [<Export("application:configurationForConnectingSceneSession:options:")>]
    member this.GetConfiguration (application : UIApplication, sceneSession : UISceneSession, options : UISceneConnectionOptions) =
        UISceneConfiguration.Create("Default Configuration", sceneSession.Role)

    override this.BuildMenu (builder) =
        base.BuildMenu (builder)
        if builder.System.Handle = UIMenuSystem.MainSystem.Handle then

            let commands (before : bool) (titlesSels : UIMenuElement[]) =
                let m = UIMenu.Create("", null, UIMenuIdentifier.None, UIMenuOptions.DisplayInline, titlesSels)
                if before then builder.InsertSiblingMenuBefore(m, UIMenuIdentifier.Close.GetConstant() |> string)
                else builder.InsertSiblingMenuAfter(m, UIMenuIdentifier.Close.GetConstant() |> string)

            let bcommands = commands true
            let acommands = commands false

            bcommands [| AppCommands.openDocument |]

            acommands [| AppCommands.saveDocument |]


    [<Export("open:")>]
    member this.Open(sender : NSObject) : unit =
        let activity = new NSUserActivity ("com.example.AppleNativeApp.open")
        let options = new UISceneActivationRequestOptions()
        UIApplication.SharedApplication.RequestSceneSessionActivation(null, activity, options, fun err ->
            ())
