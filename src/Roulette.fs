// otoshidama-radix Version 0.1.0
// https://github.com/taidalog/otoshidama-radix
// Copyright (c) 2023-2024 taidalog
// This software is licensed under the MIT License.
// https://github.com/taidalog/otoshidama-radix/blob/main/LICENSE
namespace OtoshidamaRadix

open System
open System.Diagnostics
open Browser.Dom
open Browser.Types
open Fermata
open JsNative
open RunningState

module Roulette =
    let f (value: string) (radix: string) : unit =
        value
        |> Seq.rev
        |> Seq.iteri (fun i x -> (document.getElementById $"digit%d{i + 1}").innerText <- string x)

        (document.getElementById "radix").innerText <- radix

    let randomBinary (lessThan: int) : string =
        let n = Random.lessThan lessThan
        Convert.ToString(n, 2) |> String.padLeft 11 '0'

    let randomRadix () : string =
        List.item (Random.between 0 3) [ "₍₂₎"; "₍₁₀₎"; "₍₁₆₎" ]

    let stop (intervalId: int) (value: string) (radix: string) : RunningState =
        clearInterval intervalId
        f value radix
        (document.getElementById "button" :?> HTMLButtonElement).innerText <- "Start"
        RunningState.Stopping

    let start () : RunningState =
        let n = Random.lessThan 2026
        let b = Convert.ToString(n, 2) |> String.padLeft 11 '0'
        let radix = List.item (Random.between 0 3) [ "₍₂₎"; "₍₁₀₎"; "₍₁₆₎" ]
        Debug.WriteLine $"%d{n}%s{radix}"

        let intervalId = setInterval (fun _ -> f (randomBinary 2026) (randomRadix ())) 25
        (document.getElementById "button" :?> HTMLButtonElement).innerText <- "Stop"
        RunningState.Running(intervalId, b, radix)

    let rec toggle runningState =
        match runningState with
        | RunningState.Stopping ->
            let x = start ()
            (document.getElementById "button" :?> HTMLButtonElement).onclick <- fun _ -> toggle x
        | RunningState.Running(intervalId, b, radix) ->
            let x = stop intervalId b radix
            (document.getElementById "button" :?> HTMLButtonElement).onclick <- fun _ -> toggle x
