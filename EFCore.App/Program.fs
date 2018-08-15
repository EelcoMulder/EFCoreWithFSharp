open EFCore.Model
open CompostionRoot
open System
open System.Linq

let changeName (s: Serie) = 
    let news = { s with Name = s.Name + "!" }
    updateSerie news

[<EntryPoint>] 
let main _ =       
    let episodes = [ { Id = 0; Number = 1; Season = 1; Status = EpisodeStatus.Scheduled; Name = "The start of the first beginning intro"; Description = "This episode explains everything" } ]
    let newSerie = { Id = 0; Name = "Super duper serie"; Description = "Some description of this awesome serie"; Episodes = episodes.ToList(); Status = SerieStatus.New }

    Console.WriteLine "Add a serie with an episode"
    addSerie newSerie
    
    Console.WriteLine "Retrieving the newly added episode"
    let serie = getSerie newSerie.Id
    match serie with
    | Some s -> Console.WriteLine("Serie: " + s.Description)
    | _ -> ()      
    

    Console.WriteLine "Changing the name of this serie"
    match serie with
    | Some s -> changeName s |> ignore
    | _ -> ()    
    Console.ReadKey() |> ignore
    Console.WriteLine "Deleting the serie from the database"
    match serie with
    | Some s -> deleteSerie s |> ignore
    | _ -> ()      

    Console.WriteLine "Get all series with aired episodes"
    getSeriesWithAiredEpisodes
    |> Seq.iter (fun s ->  printf "Serie: %s %d\n" s.Name s.Id)


    // Console.WriteLine "Add a serie with an episode... Synchronously" // Doesn't work with Sqlite
    // //addSerieAsync newSerie |> Async.RunSynchronously |> ignore

    Console.WriteLine "Press any key to exit"
    Console.ReadKey() |> ignore
    0
 