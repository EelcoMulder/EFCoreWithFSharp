namespace EFCore.DataAccess
open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Storage.Converters
open EFCore.Model

type SerieContext =
    inherit DbContext
    
    new() = { inherit DbContext() }
    new(options: DbContextOptions<SerieContext>) = { inherit DbContext(options) }

    override __.OnModelCreating modelBuilder = 
        let esconvert = ValueConverter<EpisodeStatus, string>((fun v -> v.ToString()), (fun v -> Enum.Parse(typedefof<EpisodeStatus>, v) :?> EpisodeStatus))
        modelBuilder.Entity<Episode>().Property(fun e -> e.Status).HasConversion(esconvert) |> ignore

        let ssconvert = ValueConverter<SerieStatus, string>((fun v -> v.ToString()), (fun v -> Enum.Parse(typedefof<SerieStatus>, v) :?> SerieStatus))
        modelBuilder.Entity<Serie>().Property(fun e -> e.Status).HasConversion(ssconvert) |> ignore      

    [<DefaultValue>]
    val mutable series:DbSet<Serie>
    member x.Series 
        with get() = x.series 
        and set v = x.series <- v

    [<DefaultValue>]
    val mutable episodes:DbSet<Episode>
    member x.Episodes 
        with get() = x.episodes 
        and set v = x.episodes <- v    
         