using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Helper
{
        public static class EntityHelperMethods
        {
            public static IQueryable<TEntity> WhereAllPropertiesOfSimilarTypeAreEqual<TEntity, TProperty>(this IQueryable<TEntity> query, TProperty value)
            {
                var param = Expression.Parameter(typeof(TEntity));

                var predicate = PredicateBuilder.False<TEntity>();

                foreach (var fieldName in GetEntityFieldsToCompareTo<TEntity, TProperty>())
                {
                    var predicateToAdd = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal(
                            Expression.PropertyOrField(param, fieldName.Key),
                            Expression.Constant(value)), param);

                    predicate = predicate.Or(predicateToAdd);
                }

                return query.Where(predicate);
            }

            public static IQueryable<TEntity> WhereAllPropertiesOfSimilarTypeContains<TEntity, TProperty>(this IQueryable<TEntity> query, TProperty value)
            {
                var param = Expression.Parameter(typeof(TEntity));
                var predicate = PredicateBuilder.False<TEntity>();
                MethodInfo contains = typeof(string).GetMethod("Contains");
                foreach (var fieldName in GetEntityFieldsToCompareTo<TEntity, TProperty>())
                {
                    if (fieldName.Value.Equals(typeof(string).Name))
                    {
                        var predicateToAdd = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Call(Expression.PropertyOrField(param, fieldName.Key),
                         contains,
                         Expression.Constant(value)), param);
                        predicate = predicate.Or(predicateToAdd);
                    }
                    else
                    {
                        var predicateToAdd = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal(
                        Expression.PropertyOrField(param, fieldName.Key),
                        Expression.Constant(value)), param);
                        predicate = predicate.Or(predicateToAdd);
                    }

                }

                return query.Where(predicate);
            }


            // TODO: You'll need to find out what fields are actually ones you would want to compare on.
            //       This might involve stripping out properties marked with [NotMapped] attributes, for
            //       for example.
            private static IDictionary<string, string> GetEntityFieldsToCompareTo<TEntity, TProperty>()
            {
                Type entityType = typeof(TEntity);
                Type propertyType = typeof(TProperty);

                var fields = entityType.GetFields()
                                    .Where(f => f.FieldType == propertyType)
                                    .ToDictionary(f => f.Name, f => f.FieldType.Name);

                var properties = entityType.GetProperties()
                                        .Where(p => p.PropertyType == propertyType)
                                        .ToDictionary(p => p.Name, p => p.PropertyType.Name);
                //return null;
                Dictionary<string, string> d3 = fields.Concat(properties).ToDictionary(e => e.Key, e => e.Value);
                return d3;
            }
        }
}
