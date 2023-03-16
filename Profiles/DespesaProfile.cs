using AluraChallenges2.Models;
using AluraChallenges2.Models.Dtos;
using AutoMapper;

namespace AluraChallenges2.Profiles;

public class DespesaProfile : Profile
{
	public DespesaProfile()
	{
		CreateMap<UpsertDespesaDto, Despesa>();
		CreateMap<Despesa, ReadDespesaDto>();
	}
}
