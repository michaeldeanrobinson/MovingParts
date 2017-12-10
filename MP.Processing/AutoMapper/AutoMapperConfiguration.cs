using AutoMapper;

namespace MP.Processing.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DefaultProfile());
            });

            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
