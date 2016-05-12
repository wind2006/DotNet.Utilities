#if (RUNNING_ON_2 || RUNNING_ON_3 || RUNNING_ON_3_5)

namespace System
{
    /// <summary>
    /// .Net 2.0,.Net 3.5下Tuple实现
    /// </summary>
    public class Tuple
    {
        #region Methods

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <returns>Tuple</returns>
        public static Tuple<T1> Create<T1>(T1 t1)
        {
            return new Tuple<T1>(t1);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns>Tuple</returns>
        public static Tuple<T1, T2> Create<T1, T2>(T1 t1, T2 t2)
        {
            return new Tuple<T1, T2>(t1, t2);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <returns>Tuple</returns>
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
        {
            return new Tuple<T1, T2, T3>(t1, t2, t3);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="t4">The t4.</param>
        /// <returns>Tuple</returns>
        private static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            return new Tuple<T1, T2, T3, T4>(t1, t2, t3, t4);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="t4">The t4.</param>
        /// <param name="t5">The t5.</param>
        /// <returns>Tuple</returns>
        private static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            return new Tuple<T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="t4">The t4.</param>
        /// <param name="t5">The t5.</param>
        /// <param name="t6">The t6.</param>
        /// <returns>Tuple</returns>
        private static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6>(t1, t2, t3, t4, t5, t6);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="t4">The t4.</param>
        /// <param name="t5">The t5.</param>
        /// <param name="t6">The t6.</param>
        /// <param name="t7">The t7.</param>
        /// <returns>Tuple</returns>
        private static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(t1, t2, t3, t4, t5, t6, t7);
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="TRest">The type of the rest.</typeparam>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="t4">The t4.</param>
        /// <param name="t5">The t5.</param>
        /// <param name="t6">The t6.</param>
        /// <param name="t7">The t7.</param>
        /// <param name="Rest">The rest.</param>
        /// <returns>Tuple</returns>
        private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Create<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, TRest Rest)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(t1, t2, t3, t4, t5, t6, t7, Rest);
        }

        #endregion Methods
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// 时间：2015-12-25 17:25
    /// 备注：
    public class Tuple<T1> : Tuple
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        public Tuple(T1 Item1)
        {
            this.Item1 = Item1;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    public class Tuple<T1, T2>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        public Tuple(T1 Item1, T2 Item2)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    public class Tuple<T1, T2, T3>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    public class Tuple<T1, T2, T3, T4>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3, T4}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        /// <param name="Item4">The item4.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3, T4 Item4)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item4.
        /// </summary>
        public T4 Item4
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    public class Tuple<T1, T2, T3, T4, T5>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3, T4, T5}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        /// <param name="Item4">The item4.</param>
        /// <param name="Item5">The item5.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3, T4 Item4, T5 Item5)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
            this.Item5 = Item5;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item4.
        /// </summary>
        public T4 Item4
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item5.
        /// </summary>
        public T5 Item5
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    public class Tuple<T1, T2, T3, T4, T5, T6>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3, T4, T5, T6}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        /// <param name="Item4">The item4.</param>
        /// <param name="Item5">The item5.</param>
        /// <param name="Item6">The item6.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3, T4 Item4, T5 Item5, T6 Item6)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
            this.Item5 = Item5;
            this.Item6 = Item6;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item4.
        /// </summary>
        public T4 Item4
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item5.
        /// </summary>
        public T5 Item5
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item6.
        /// </summary>
        public T6 Item6
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    /// <typeparam name="T7">The type of the 7.</typeparam>
    public class Tuple<T1, T2, T3, T4, T5, T6, T7>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        /// <param name="Item4">The item4.</param>
        /// <param name="Item5">The item5.</param>
        /// <param name="Item6">The item6.</param>
        /// <param name="Item7">The item7.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3, T4 Item4, T5 Item5, T6 Item6, T7 Item7)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
            this.Item5 = Item5;
            this.Item6 = Item6;
            this.Item7 = Item7;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item4.
        /// </summary>
        public T4 Item4
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item5.
        /// </summary>
        public T5 Item5
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item6.
        /// </summary>
        public T6 Item6
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item7.
        /// </summary>
        public T7 Item7
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Tuple
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    /// <typeparam name="T7">The type of the 7.</typeparam>
    /// <typeparam name="TRest">The type of the rest.</typeparam>
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> class.
        /// </summary>
        /// <param name="Item1">The item1.</param>
        /// <param name="Item2">The item2.</param>
        /// <param name="Item3">The item3.</param>
        /// <param name="Item4">The item4.</param>
        /// <param name="Item5">The item5.</param>
        /// <param name="Item6">The item6.</param>
        /// <param name="Item7">The item7.</param>
        /// <param name="Rest">The rest.</param>
        public Tuple(T1 Item1, T2 Item2, T3 Item3, T4 Item4, T5 Item5, T6 Item6, T7 Item7, TRest Rest)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
            this.Item5 = Item5;
            this.Item6 = Item6;
            this.Item7 = Item7;
            this.Rest = Rest;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the item1.
        /// </summary>
        public T1 Item1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item2.
        /// </summary>
        public T2 Item2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item3.
        /// </summary>
        public T3 Item3
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item4.
        /// </summary>
        public T4 Item4
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item5.
        /// </summary>
        public T5 Item5
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item6.
        /// </summary>
        public T6 Item6
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item7.
        /// </summary>
        public T7 Item7
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the rest.
        /// </summary>
        public TRest Rest
        {
            get; set;
        }

        #endregion Properties
    }
}

#endif