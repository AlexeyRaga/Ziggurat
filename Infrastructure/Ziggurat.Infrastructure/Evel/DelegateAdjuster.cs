using System;
using System.Linq.Expressions;

namespace Ziggurat.Infrastructure
{
    class DelegateAdjuster
    {
        public static Action<BaseT> CastArgument<BaseT, DerivedT>(Expression<Action<DerivedT>> source) where DerivedT : BaseT
        {
            if (typeof(DerivedT) == typeof(BaseT))
            {
                return (Action<BaseT>)((Delegate)source.Compile());
            }
            ParameterExpression sourceParameter = Expression.Parameter(typeof(BaseT), "source");
            var result = Expression.Lambda<Action<BaseT>>(
                Expression.Invoke(
                    source,
                    Expression.Convert(sourceParameter, typeof(DerivedT))),
                sourceParameter);
            return result.Compile();
        }

        public static Action<BaseT> CastArgument<BaseT>(object instance, Type derivedType, string methodName)
        {
            var baseType = typeof(BaseT);

            var parameter = Expression.Parameter(baseType, "message");

            var instanceExpression = Expression.Constant(instance);

            var callExpression = Expression.Call(
              instanceExpression,
              methodName,
              null,
              Expression.Convert(parameter, derivedType));

            var lambdaCall = Expression.Lambda<Action<BaseT>>(callExpression, parameter);
            return lambdaCall.Compile();
        }
    }

}
