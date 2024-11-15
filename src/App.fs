﻿// otoshidama-radix Version 0.1.0
// https://github.com/taidalog/otoshidama-radix
// Copyright (c) 2023-2024 taidalog
// This software is licensed under the MIT License.
// https://github.com/taidalog/otoshidama-radix/blob/main/LICENSE

open System
open Browser.Dom
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Fermata

module App =
    let random () : int =
        let rand = new Random()
        rand.Next()

    let randomLessThan x : int =
        let rand = new Random()
        rand.Next(x)

    let randomBetween x y : int =
        let rand = new Random()
        rand.Next(x, y)

    let start () =
        let outputArea = document.getElementById "outputArea"
        let n = randomLessThan 2026
        let b = Convert.ToString(n, 2) |> String.padLeft 11 '0'
        outputArea.innerText <- $"%s{b}₍₂₎"

    let stop' x =
        let outputArea = document.getElementById "outputArea"
        outputArea.innerText <- x

    let stop () = stop' "hey"

    let keyboardshortcut (e: KeyboardEvent) =
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

            if not isHelpWindowActive && not isInformationPolicyWindowActive then
                stop ()
        | "Enter" -> start ()
        | "?" ->
            if not isHelpWindowActive then
                helpWindow.classList.add "active"
        | _ -> ()

    window.addEventListener (
        "DOMContentLoaded",
        (fun _ ->
            (document.getElementById "button" :?> HTMLButtonElement).innerText <- "Start"

            start ()

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

            (document.getElementById "button" :?> HTMLButtonElement).onclick <- fun _ -> start ())
    )
