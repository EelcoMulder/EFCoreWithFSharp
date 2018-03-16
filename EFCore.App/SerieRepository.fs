namespace EFCore.DataAccess
open Microsoft.EntityFrameworkCore
open EFCore.DataAccess
open EFCore.Model

module SerieRepository =
    let getSerie (context: SerieContext) id =
        context.Series.Include(fun s -> s.Episodes) 
        |> Seq.tryFind (fun f -> f.Id = id) 

    let getEpisode (context: SerieContext) id =
        context.Episodes
        |> Seq.tryFind (fun f -> f.Id = id) 

    let addSerieAsync (context: SerieContext) (entity: Serie) =
        async {
            context.Series.AddAsync(entity) |> Async.AwaitTask |> ignore
            let! _ = context.SaveChangesAsync true |> Async.AwaitTask
            return entity
        }   

    let addSerie (context: SerieContext) (entity: Serie) =
        context.Series.Add(entity) |> ignore
        context.SaveChanges |> ignore

    let updateSerie (context: SerieContext) (entity: Serie) = 
        let currentEntry = context.Series.Find(entity.Id)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges |> ignore   

    let deleteSerie (context: SerieContext) (entity: Serie) = 
        context.Series.Remove entity |> ignore
        context.SaveChanges |> ignore

    let getSeriesWithAiredEpisodes (context: SerieContext) = 
        query {
            for serie in context.Series do
                where (serie.Episodes.Exists (fun e -> e.Status = EpisodeStatus.Aired))
                select serie 
        }

