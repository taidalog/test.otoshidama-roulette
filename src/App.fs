// otoshidama-radix Version 0.2.0
// https://github.com/taidalog/otoshidama-radix
// Copyright (c) 2023-2024 taidalog
// This software is licensed under the MIT License.
// https://github.com/taidalog/otoshidama-radix/blob/main/LICENSE
namespace OtoshidamaRadix

open System
open Browser.Dom
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open RunningState
open Roulette

module App =
    let keyboardshortcut (e: KeyboardEvent) : unit =
        let helpWindow = document.getElementById "helpWindow"

        let isHelpWindowActive =
            helpWindow.classList |> JS.Constructors.Array?from |> Array.contains "active"

        let informationPolicyWindow = document.getElementById "informationPolicyWindow"

        let isInformationPolicyWindowActive =
            informationPolicyWindow.classList
            |> JS.Constructors.Array?from
            |> Array.contains "active"

        match e.key with
        | "Escape" ->
            if isHelpWindowActive then
                helpWindow.classList.remove "active"

            if isInformationPolicyWindowActive then
                informationPolicyWindow.classList.remove "active"
        | "?" ->
            if not isHelpWindowActive then
                helpWindow.classList.add "active"
        | _ -> ()

    window.addEventListener (
        "DOMContentLoaded",
        (fun _ ->
            Roulette.display "00000000000" "₍₂₎"

            let button = document.getElementById "button" :?> HTMLButtonElement
            button.innerText <- "始"

            let message = document.getElementById "message"
            message.innerText <- "お年玉ルーレット！"

            // help window
            [ "helpButton"; "helpClose" ]
            |> List.iter (fun x ->
                (document.getElementById x :?> HTMLButtonElement).onclick <-
                    fun _ -> (document.getElementById "helpWindow").classList.toggle "active" |> ignore)

            // information policy window
            (document.getElementById "informationPolicyLink").onclick <-
                fun event ->
                    event.preventDefault ()
                    (document.getElementById "informationPolicyWindow").classList.add "active"

            (document.getElementById "informationPolicyClose").onclick <-
                fun _ -> (document.getElementById "informationPolicyWindow").classList.remove "active"

            // keyboard shortcut
            document.onkeydown <- fun (e: KeyboardEvent) -> keyboardshortcut e

            button.onclick <- fun _ -> Roulette.toggle RunningState.Stopping)
    )
