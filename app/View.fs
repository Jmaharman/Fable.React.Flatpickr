module App.View

open App.Types

open Fulma
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Core.JsInterop

let menuItem page current label dispatch =
    li [ Common.classes [ "is-active", current = page ]; 
         OnClick (fun _ -> dispatch (View page)) ]
       [ a [ ] [ str label ] ]

let sidebar state dispatch = 
    Menu.menu [ ] 
        [ img [ Src "https://fable-elmish.github.io/elmish/img/logo.png" ] 
          Menu.label [ ] [ str "General" ]
          Menu.list [ ] 
               [ menuItem Introduction state.CurrentPage "Introduction" dispatch
                 menuItem Usage state.CurrentPage "Usage" dispatch ] ]

let introduction = 
    div [ ]  
        [ h1 [ Style [ FontSize 30 ] ] [ str "Fable.React.Flatpickr" ]
          p  [ ] [ str "Fable binding for react-flatpickr that is ready to use within Elmish applications" ] 
          br [ ] 
          Common.highlight """type State = { SelectedTime : DateTime }

type Msg = UpdateSelectedTime of DateTime 

let init() = { SelectedTime = DateTime.Now }, Cmd.none

let update msg state = 
    match msg with 
    | UpdateSelectedTime time ->
        let nextState = { state with SelectedTime = time }
        nextState, Cmd.none

let render state dispatch = 
    Flatpickr.flatpickr 
        [ Flatpickr.Value state.SelectedTime 
          Flatpickr.OnChange (UpdateSelectedTime >> dispatch)
          Flatpickr.ClassName "input" ]
          """
          br [ ] 
          hr [ ]
          h1 [ Style [ FontSize 30 ] ] [ str "Examples and configurations" ]
          br [ ] ]

let spacing = Props.Style [ Props.Padding 20 ]

let render (state: State) dispatch = 
    Columns.columns [ ] 
        [ Column.column [ Column.Width (Screen.All, Column.Is2) ] 
                        [ div [ spacing ] [ sidebar state dispatch ] ] 
          Column.column [ Column.Width (Screen.All, Column.Is7) ] 
                        [ div [ spacing ] 
                              [ introduction
                                Components.Flatpickr.View.render state.Flatpickr (FlatpickrMsg >> dispatch) ]  ] ]