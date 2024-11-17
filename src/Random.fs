// otoshidama-radix Version 0.1.0
// https://github.com/taidalog/otoshidama-radix
// Copyright (c) 2023-2024 taidalog
// This software is licensed under the MIT License.
// https://github.com/taidalog/otoshidama-radix/blob/main/LICENSE
namespace OtoshidamaRadix

open System

module Random =
    let next () : int =
        let rand = new Random()
        rand.Next()

    let lessThan (x: int) : int =
        let rand = new Random()
        rand.Next(x)

    let between (x: int) (y: int) : int =
        let rand = new Random()
        rand.Next(x, y)
