open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.StaticFiles
open Giraffe

let webApp =
    choose [
        GET >=> route "/" >=> htmlFile "index.html"
        setStatusCode 404 >=> text "Not Found"
    ]

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddGiraffe() |> ignore
    let app = builder.Build()

    app.UseDefaultFiles() |> ignore
    app.UseStaticFiles()  |> ignore

    app.UseGiraffe webApp
    printfn "🚀 Listening on http://localhost:5000"
    app.Run("http://localhost:5000")
    0
