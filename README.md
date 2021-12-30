# ArgsAsRoutes

A library to parse command line arguments as if they where routes in web applications

## What?

You describe your arguments very similar to the way an application would normally describe its arguments to you.
Just that you find a $ before everything that you want to extract. 

```csharp
  var parser = new CommandLineParser(
      args, 
      "FTPdeploy is a program that helps you with your ftp based deployment");

  parser.On("describe-dir --local $local-dir [--ignores $ignores] --output $output")
      .Execute((parsedArgs, parser) => {
          Console.WriteLine("Yey! You have a match.");
          Console.WriteLine(parsedArgs.ToString());
      });

  parser.On("describe-dir --ftp $ftp [--ignores $ignores] --output $output").
         Execute((parsedArgs, parser) => {
              Console.WriteLine("Yey! You have a match.");
              Console.WriteLine(parsedArgs.ToString());
          });

  parser.Run();
```

If a route is found "complete" the matching delegate is invoked.

- [features and state of implementation](Documentation/Requirements.md)
