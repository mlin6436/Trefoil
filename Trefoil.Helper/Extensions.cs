using AutoMapper;
using Ninject.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// Binds a service contract to a matching service endpoint.
        /// </summary>
        /// <typeparam name="T">The type of service contract</typeparam>
        /// <param name="serviceContract">The service contract</param>
        public static void ToDefaultChannel<T>(this IBindingToSyntax<T> serviceContract) where T : class
        {
            serviceContract.ToMethod(c => { return new ChannelFactory<T>("*").CreateChannel(); });
        }

        /// <summary>
        /// Gets the root exception for the current exception.
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>The root exception of the current exception</returns>
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerException.GetOriginalException();
            }

            return ex;
        }

        /// <summary>
        /// Maps a source object to a destination object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object</typeparam>
        /// <typeparam name="TDestination">The type of destination object</typeparam>
        /// <param name="source">The source object</param>
        /// <returns>The target object</returns>
        public static TDestination MapObject<TSource, TDestination>(this TSource source)
        {
            Mapper.CreateMap<TSource, TDestination>();

            // TODO: ignore primanry key.

            foreach (var destinationPropertyName in typeof(TDestination).GetProperties().Select(t => t.Name))
            {
                if (!typeof(TSource).GetProperties().Select(t => t.Name).Contains(destinationPropertyName))
                {
                    Mapper.CreateMap<TSource, TDestination>().ForMember(destinationPropertyName, opt => opt.Ignore());
                }
            }

            Mapper.AssertConfigurationIsValid();

            return Mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Maps a collection of source objects to a collection of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of source object</typeparam>
        /// <typeparam name="TDestination">The type of destination object</typeparam>
        /// <param name="source">The collection of source objects</param>
        /// <returns>The collection of target objects</returns>
        public static IEnumerable<TDestination> MapObjects<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            List<TDestination> list = new List<TDestination>();

            foreach (TSource item in source)
            {
                list.Add(item.MapObject<TSource, TDestination>());
            }

            return list.ToArray();
        }
    }
}
