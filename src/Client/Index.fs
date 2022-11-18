module Index

open Elmish
open Feliz
open Feliz.Router

type Url =
    | Page1
    | Page2

type Page =
    | Page1 of Page1.Model
    | Page2 of Page2.Model
    | NotFound

type Model =
    {
        CurrentUrl : Url option
        CurrentPage : Page
    }

type Msg =
    | Page1Msg of Page1.Msg
    | Page2Msg of Page2.Msg
    | UrlChanged of Url option

let tryParseUrl = function
    | [] | [ "page1" ] -> Some Url.Page1
    | [ "page2" ] -> Some Url.Page2
    | _ -> None

let initPage url =
    match url with
    | Some Url.Page1 ->
        let page1Model, page1Msg = Page1.init ()
        { CurrentUrl = url; CurrentPage = (Page.Page1 page1Model) }, page1Msg |> Cmd.map Page1Msg
    | Some Url.Page2 ->
        let page2Model, page2Msg = Page2.init ()
        { CurrentUrl = url; CurrentPage = (Page.Page2 page2Model) }, page2Msg |> Cmd.map Page2Msg
    | None ->
        { CurrentUrl = url; CurrentPage = Page.NotFound }, Cmd.none

let init () : Model * Cmd<Msg> =
    Router.currentPath()
    |> tryParseUrl
    |> initPage

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg, model.CurrentPage with
    | Page1Msg page1Msg, Page.Page1 page1Model  ->
        let newPage1Model, newPage1Msg = Page1.update page1Msg page1Model
        { model with CurrentPage = Page.Page1 newPage1Model }, newPage1Msg |> Cmd.map Page1Msg
    | Page2Msg page2Msg, Page.Page2 page2Model  ->
        let newPage2Model, newPage2Msg = Page2.update page2Msg page2Model
        { model with CurrentPage = Page.Page2 newPage2Model }, newPage2Msg |> Cmd.map Page2Msg
    | UrlChanged urlSegments, _ ->
        initPage urlSegments
    | _ ->
        model, Cmd.none

let view (model: Model) (dispatch: Msg -> unit) =
    React.router [
        router.pathMode
        router.onUrlChanged (tryParseUrl >> UrlChanged >> dispatch)
        router.children [
            match model.CurrentPage with
            | Page.Page1 page1Model ->
                Page1.view page1Model (Page1Msg >> dispatch)
            | Page.Page2 page2Model ->
                Page2.view page2Model (Page2Msg >> dispatch)
            | Page.NotFound ->
                Html.p "Not found"
        ]
    ]