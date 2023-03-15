using AluraChallenges2.Models;
using AluraChallenges2.Models.Dtos;
using AutoMapper;

namespace AluraChallenges2.Profiles;

public class Receitaprofile : Profile
{
	public Receitaprofile()
	{
		CreateMap<UpsertReceitaDto, Receita>();
		CreateMap<Receita, ReadReceitaDto>();
	}
}
