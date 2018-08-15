namespace EFCore.DataAccess
open EFCore.DataAccess
open EFCore.Model
open System.Linq
open Microsoft.EntityFrameworkCore

module SerieRepository =
    let getSerie (context: SerieContext) id =
        query {
            for serie in context.Series do
                where (serie.Id = id)
                select serie 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getEpisode (context: SerieContext) id =
        query {
            for episode in context.Episodes do
                where (episode.Id = id)
                select episode 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)    

    let getEpisodeLinq (context: SerieContext) id =
        query {
            for episode in context.Episodes do
                where (episode.Id = id)
                select episode 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)  

    let addSerieAsync (context: SerieContext) (entity: Serie) =
        async {
            context.Series.AddAsync(entity) |> Async.AwaitTask |> ignore
            let! _ = context.SaveChangesAsync true |> Async.AwaitTask
            return entity
        }    

    let addSerie (context: SerieContext) (entity: Serie) =
        context.Series.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let updateSerie (context: SerieContext) (entity: Serie) = 
        let currentEntry = context.Series.Find(entity.Id)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore

    let deleteSerie (context: SerieContext) (entity: Serie) = 
        context.Series.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let getSeriesWithAiredEpisodes (context: SerieContext) = 
        query {
            for serie in context.Series do
                where (serie.Episodes.Exists (fun e -> e.Status = EpisodeStatus.Aired))
                select serie 
        }

