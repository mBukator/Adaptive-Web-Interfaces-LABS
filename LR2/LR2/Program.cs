using System;

using TextExample = LR2.Functions.TextOperations;
using LinqExample = LR2.Functions.LinqOperations;
using IOExample = LR2.Functions.ExampleIO;
using TimerExample = LR2.Functions.TimerOperations;
using XMLExample = LR2.Functions.XmlOperations;


class Program {
    static void Main() {
        IOExample.Example_IO();
        LinqExample.ExampleLinq();
        TextExample.ExampleText();
        TimerExample.ExampleTimer();
        XMLExample.ExampleXml();
    }    
}