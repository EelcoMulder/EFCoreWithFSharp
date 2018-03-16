namespace EFCore.Model

open System.Collections.Generic
open System.ComponentModel.DataAnnotations.Schema

type EpisodeStatus =
  | Scheduled = 0
  | Aired = 1

type EpisodeId = int

type SerieStatus = 
  | New = 0
  | Running = 1
  | Ended = 2

type SerieId = int

type [<CLIMutable>] Serie =
  { Id: SerieId
    Name: string
    Description: string
    Episodes: List<Episode>
    Status: SerieStatus
  } 
and [<CLIMutable>] Episode = 
  { Id: EpisodeId
    Number: int
    Season : int
    Name: string
    Description: string
    Status: EpisodeStatus
    [<NotMapped>]Serie: Option<Serie>
  }


