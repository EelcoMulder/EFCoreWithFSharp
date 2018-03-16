module CompostionRoot

open EFCore.DataAccess
open Microsoft.EntityFrameworkCore
let configureSqliteContext = 
    (fun () ->
        let optionsBuilder = new DbContextOptionsBuilder<SerieContext>();
        optionsBuilder.UseSqlite(@"Data Source=D:\Db\Series.db;")|> ignore
        new SerieContext(optionsBuilder.Options)
    )

let configureSqlServerContext = 
    (fun () ->
        let optionsBuilder = new DbContextOptionsBuilder<SerieContext>();
        optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;") |> ignore
        new SerieContext(optionsBuilder.Options)
    )

let getContext = configureSqlServerContext()
let getSerie  = SerieRepository.getSerie getContext
let getEpisode = SerieRepository.getEpisode getContext
let addSerie = SerieRepository.addSerie getContext
let addSerieAsync = SerieRepository.addSerieAsync getContext
let updateSerie = SerieRepository.updateSerie getContext
let deleteSerie = SerieRepository.deleteSerie getContext
let getSeriesWithAiredEpisodes = SerieRepository.getSeriesWithAiredEpisodes getContext
