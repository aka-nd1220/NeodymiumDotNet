using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    ///     Represents an expression comparison operation.
    /// </summary>
    public class ExpressionComparer
        : IEqualityComparer<Expression           >
        , IEqualityComparer<ConstantExpression   >
        , IEqualityComparer<ParameterExpression  >
        , IEqualityComparer<UnaryExpression      >
        , IEqualityComparer<BinaryExpression     >
        , IEqualityComparer<MemberExpression     >
        , IEqualityComparer<MethodCallExpression >
        , IEqualityComparer<InvocationExpression >
        , IEqualityComparer<ConditionalExpression>
        , IEqualityComparer<NewExpression        >
        , IEqualityComparer<LambdaExpression     >
        , IEqualityComparer<BlockExpression      >
        , IEqualityComparer<LoopExpression       >
        , IEqualityComparer<LabelExpression      >
        , IEqualityComparer<NewArrayExpression   >
        , IEqualityComparer<DefaultExpression    >
        , IEqualityComparer<IndexExpression      >
    {
        /// <summary>
        ///     Returns an instance of <see cref="ExpressionComparer"/>.
        /// </summary>
        public static IEqualityComparer<Expression> Instance { get; }
            = new ExpressionComparer();
        private ExpressionComparer() { }


        #region GetHashCode

        private int BitRotate(int value)
        {
            var xvalue = (uint)value;
            return (int)((xvalue << 19) | (xvalue >> 13));
        }


        private int MixHash(int x1, int x2)
            => BitRotate(x1) ^ x2;


        private int MixHash(int x1, int x2, int x3)
            => MixHash(MixHash(x1, x2), x3);


        private int MixHash(int x1, int x2, int x3, int x4)
            => MixHash(MixHash(MixHash(x1, x2), x3), x4);



        private int GetHashCodeBase(Expression obj)
            => MixHash(
                obj.Type.GetHashCode(),
                obj.NodeType.GetHashCode());


        private int GetHashCode(IReadOnlyList<Expression> obj)
            => obj.Select(GetHashCode).Aggregate(0, MixHash);


        private int GetHashCode(LabelTarget obj)
            => MixHash(
                obj.Type.GetHashCode(),
                obj.Name.GetHashCode());


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(Expression obj)
            => obj switch
            {
                ConstantExpression    expr => GetHashCode(expr),
                ParameterExpression   expr => GetHashCode(expr),
                UnaryExpression       expr => GetHashCode(expr),
                BinaryExpression      expr => GetHashCode(expr),
                MemberExpression      expr => GetHashCode(expr),
                MethodCallExpression  expr => GetHashCode(expr),
                InvocationExpression  expr => GetHashCode(expr),
                ConditionalExpression expr => GetHashCode(expr),
                NewExpression         expr => GetHashCode(expr),
                LambdaExpression      expr => GetHashCode(expr),
                BlockExpression       expr => GetHashCode(expr),
                LoopExpression        expr => GetHashCode(expr),
                LabelExpression       expr => GetHashCode(expr),
                NewArrayExpression    expr => GetHashCode(expr),
                DefaultExpression     expr => GetHashCode(expr),
                IndexExpression       expr => GetHashCode(expr),
                _ => 0,
            };


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ConstantExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Value?.GetHashCode() ?? 0);


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ParameterExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Name.GetHashCode());


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(UnaryExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.NodeType.GetHashCode(),
                GetHashCode(obj.Operand));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(BinaryExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.NodeType.GetHashCode(),
                GetHashCode(obj.Left),
                GetHashCode(obj.Right));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(MemberExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Member.GetHashCode(),
                GetHashCode(obj.Expression));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(MethodCallExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Method.GetHashCode(),
                GetHashCode(obj.Object),
                GetHashCode(obj.Arguments));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(InvocationExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Expression),
                GetHashCode(obj.Arguments));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ConditionalExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Test),
                GetHashCode(obj.IfTrue),
                GetHashCode(obj.IfFalse));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(NewExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Constructor.GetHashCode(),
                GetHashCode(obj.Arguments));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(LambdaExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Parameters),
                GetHashCode(obj.Body));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(BlockExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Variables),
                GetHashCode(obj.Expressions),
                GetHashCode(obj.Result));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(LoopExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Body));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(LabelExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
               GetHashCode(obj.Target),
               GetHashCode(obj.DefaultValue));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(NewArrayExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                GetHashCode(obj.Expressions));


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(DefaultExpression obj)
            => GetHashCodeBase(obj);


        /// <summary>
        ///     Calculates the hash value of the specified expression.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(IndexExpression obj)
            => MixHash(
                GetHashCodeBase(obj),
                obj.Indexer.GetHashCode(),
                GetHashCode(obj.Object),
                GetHashCode(obj.Arguments));

        #endregion


        #region Equals

        private bool EqualsBase(
            Expression x,
            Expression y)
            => x.Type == y.Type
            && x.NodeType == y.NodeType;


        private bool Equals(
            IReadOnlyList<Expression> x,
            IReadOnlyList<Expression> y)
        {
            if(x.Count != y.Count)
                return false;
            for(int i = 0, count = x.Count; i < count; ++i)
            {
                if(!Equals(x[i], y[i]))
                    return false;
            }
            return true;
        }


        private bool Equals(
            LabelTarget x,
            LabelTarget y)
            => x.Type == y.Type
            && x.Name == y.Name;


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            Expression x,
            Expression y)
            => (x, y) switch
            {
                (ConstantExpression    xx, ConstantExpression    yy) => Equals(xx, yy),
                (ParameterExpression   xx, ParameterExpression   yy) => Equals(xx, yy),
                (UnaryExpression       xx, UnaryExpression       yy) => Equals(xx, yy),
                (BinaryExpression      xx, BinaryExpression      yy) => Equals(xx, yy),
                (MemberExpression      xx, MemberExpression      yy) => Equals(xx, yy),
                (MethodCallExpression  xx, MethodCallExpression  yy) => Equals(xx, yy),
                (InvocationExpression  xx, InvocationExpression  yy) => Equals(xx, yy),
                (ConditionalExpression xx, ConditionalExpression yy) => Equals(xx, yy),
                (NewExpression         xx, NewExpression         yy) => Equals(xx, yy),
                (LambdaExpression      xx, LambdaExpression      yy) => Equals(xx, yy),
                (BlockExpression       xx, BlockExpression       yy) => Equals(xx, yy),
                (LoopExpression        xx, LoopExpression        yy) => Equals(xx, yy),
                (LabelExpression       xx, LabelExpression       yy) => Equals(xx, yy),
                (NewArrayExpression    xx, NewArrayExpression    yy) => Equals(xx, yy),
                (DefaultExpression     xx, DefaultExpression     yy) => Equals(xx, yy),
                (IndexExpression       xx, IndexExpression       yy) => Equals(xx, yy),
                (null, null) => true,
                _ => false,
            };


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            ConstantExpression x,
            ConstantExpression y)
            => EqualsBase(x, y)
            && Equals(x.Value, y.Value);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            ParameterExpression x,
            ParameterExpression y)
            => EqualsBase(x, y)
            && x.Name == y.Name;


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            UnaryExpression x,
            UnaryExpression y)
            => EqualsBase(x, y)
            && x.NodeType == y.NodeType
            && Equals(x.Operand, y.Operand);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            BinaryExpression x,
            BinaryExpression y)
            => EqualsBase(x, y)
            && x.NodeType == y.NodeType
            && Equals(x.Left, y.Left)
            && Equals(x.Right, y.Right);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            MemberExpression x,
            MemberExpression y)
            => EqualsBase(x, y)
            && x.Member == y.Member
            && Equals(x.Expression, y.Expression);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            MethodCallExpression x,
            MethodCallExpression y)
            => EqualsBase(x, y)
            && x.Method == y.Method
            && Equals(x.Object, y.Object)
            && Equals(x.Arguments, y.Arguments);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            InvocationExpression x,
            InvocationExpression y)
            => EqualsBase(x, y)
            && Equals(x.Expression, y.Expression)
            && Equals(x.Arguments, y.Arguments);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            ConditionalExpression x,
            ConditionalExpression y)
            => EqualsBase(x, y)
            && Equals(x.Test, y.Test)
            && Equals(x.IfTrue, y.IfTrue)
            && Equals(x.IfFalse, y.IfFalse);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            NewExpression x,
            NewExpression y)
            => EqualsBase(x, y)
            && x.Constructor == y.Constructor
            && Equals(x.Arguments, y.Arguments);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            LambdaExpression x,
            LambdaExpression y)
            => EqualsBase(x, y)
            && Equals(x.Parameters, y.Parameters)
            && Equals(x.Body, y.Body);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            BlockExpression x,
            BlockExpression y)
            => EqualsBase(x, y)
            && Equals(x.Variables, y.Variables)
            && Equals(x.Expressions, y.Expressions)
            && Equals(x.Result, y.Result);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            LoopExpression x,
            LoopExpression y)
            => EqualsBase(x, y)
            && Equals(x.Body, y.Body);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            LabelExpression x,
            LabelExpression y)
            => EqualsBase(x, y)
            && Equals(x.Target, y.Target)
            && Equals(x.DefaultValue, y.DefaultValue);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            NewArrayExpression x,
            NewArrayExpression y)
            => EqualsBase(x, y)
            && Equals(x.Expressions, y.Expressions);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            DefaultExpression x,
            DefaultExpression y)
            => EqualsBase(x, y);


        /// <summary>
        ///     Determines whether two expressions are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(
            IndexExpression x,
            IndexExpression y)
            => EqualsBase(x, y)
            && x.Indexer == y.Indexer
            && Equals(x.Object, y.Object)
            && Equals(x.Arguments, y.Arguments);

        #endregion
    }
}
