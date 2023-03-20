using AluraChallenges2.Models;
using AluraChallenges2.Models.Dtos;
using AutoMapper;

namespace AluraChallenges2.Profiles;

public class CategoriaProfile : Profile
{
	public CategoriaProfile()
	{
		CreateMap<Categoria, ReadCategoriaDto>();
	}
}
