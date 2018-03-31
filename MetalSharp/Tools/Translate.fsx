#load "../X86/Translate.fsx"
#load "../ARM/Translate.fsx"

open Common

open System.IO

let path arch = Path.Combine(__SOURCE_DIRECTORY__, "..", arch)

let translate arch f =
    for inputPath in Directory.EnumerateFiles(path arch, "*.dat") do
        let outputPath = Path.ChangeExtension(inputPath, ".fs")
        
        use outputFile = File.Create outputPath
        use output = new StreamWriter(outputFile, utf8)

        output.AutoFlush <- true

        fprintfn output """namespace MetalSharp.%s

open System

open MetalSharp

module Typed =
"""
            arch

        f (File.ReadLines inputPath) output

do translate "ARM" ARM.translate
   translate "X86" X86.translate
