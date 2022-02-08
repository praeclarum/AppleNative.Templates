module Tests.Program

open System
open System.Reflection
open NUnit.Framework

type AssemblySigil() =
    let x = 12

let hasAttr attrType (memberInfo : MemberInfo) =
    memberInfo.GetCustomAttribute(attrType) <> null

let mutable numSuccess = 0
let mutable numFailures = 0

let testSuccess (m : MethodInfo) =
    printfn "+ %s" m.Name
    numSuccess <- numSuccess + 1

let runTests (testType : Type) (testMethods : MethodInfo[]) =
    printfn "# %s" testType.FullName
    for m in testMethods do
        try
            let result = m.Invoke(null, Array.empty)
            testSuccess m
        with ex ->
            let mutable iex = ex
            while iex.InnerException <> null do
                iex <- iex.InnerException
            match iex with
            | :? NUnit.Framework.SuccessException ->
                testSuccess m
            | _ ->
                printfn "- %s: %s: %s" m.Name (iex.GetType().FullName) iex.Message
                numFailures <- numFailures + 1

[<EntryPoint>]
let main _ =
    let asm = typeof<AssemblySigil>.Assembly
    let types = asm.GetTypes()
    let testType = typeof<NUnit.Framework.TestAttribute>
    let isTest = hasAttr testType
    for t in types do
        let methods = t.GetMethods()
        let tests =
            methods
            |> Array.filter isTest
        if tests.Length > 0 then
            runTests t tests
    printfn "Tests: %d, Successes: %d, Failures: %d" (numSuccess + numFailures) numSuccess numFailures
    if numFailures > 0 then 100 else 0
