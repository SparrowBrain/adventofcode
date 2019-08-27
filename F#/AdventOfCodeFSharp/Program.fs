open System.IO
open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.


let applyFrequencyChange x y = x + y

  

let rec solve01 (reader:StreamReader) x =    
    let line = reader.ReadLine()
    if (line = null) then
        x
    else
        Int32.Parse line
        |> applyFrequencyChange x  
        |> solve01 reader
        

    

[<EntryPoint>]
let main argv =     
    use streamReader = new StreamReader("Day01\\input.txt")
    solve01 streamReader 0
    |> printfn "%i"    
    0 // return an integer exit code
