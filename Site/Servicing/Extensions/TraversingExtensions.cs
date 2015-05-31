using System;
using JetBrains.Annotations;

namespace Servicing.Extensions
{
    public static class TraversingExtensions
    {
        public static TValue Dot<TEntity, TValue>(this TEntity entity, [NotNull] Func<TEntity, TValue> evaluate)
            where TEntity : class where TValue : class
        {
            if (evaluate == null)
            {
                throw new ArgumentNullException("evaluate");
            }
            return entity == null ? null : evaluate(entity);
        }

        public static TValue Dot<TEntity, TValue>(this TEntity? entity, [NotNull] Func<TEntity, TValue> evaluate)
            where TEntity : struct where TValue : class
        {
            if (evaluate == null)
            {
                throw new ArgumentNullException("evaluate");
            }
            return entity == null ? null : evaluate(entity.Value);
        }

        public static TValue? Dot<TEntity, TValue>(this TEntity entity, [NotNull] Func<TEntity, TValue?> evaluate)
            where TEntity : class where TValue : struct
        {
            if (evaluate == null)
            {
                throw new ArgumentNullException("evaluate");
            }
            return entity == null ? null : evaluate(entity);
        }

        public static TValue? Dot<TEntity, TValue>(this TEntity? entity, [NotNull] Func<TEntity, TValue?> evaluate)
            where TEntity : struct where TValue : struct
        {
            if (evaluate == null)
            {
                throw new ArgumentNullException("evaluate");
            }
            return entity == null ? null : evaluate(entity.Value);
        }
    }
}
