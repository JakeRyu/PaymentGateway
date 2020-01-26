using AutoMapper;

namespace Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Utilise the default implementation of an interface.
        /// By using this interface, it's not necessary to create a mapping profile unless it needs special one.
        /// </summary>
        /// <param name="profile">Auto mapper profile</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}