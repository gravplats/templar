using System.Collections.Generic;
using NUnit.Framework;

namespace Templar.Tests
{
    public static class AssertExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected, string message = null, params object[] args)
        {
            Assert.AreEqual(expected, actual, message, args);
        }

        public static void ShouldNotBe<T>(this T actual, T expected, string message = null, params object[] args)
        {
            Assert.AreNotEqual(expected, actual, message, args);
        }

        public static void ShouldBeFalse(this bool condition)
        {
            Assert.False(condition);
        }

        public static void ShouldBeTrue(this bool condition)
        {
            Assert.True(condition);
        }

        public static void ShouldBeNull(this object obj, string message = null, params object[] args)
        {
            Assert.Null(obj, message, args);
        }

        public static void ShouldNotBeNull(this object obj, string message = null, params object[] args)
        {
            Assert.NotNull(obj, message, args);
        }

        public static void ShouldBeNullOrEmpty(this string obj, string message = null, params object[] args)
        {
            Assert.IsNullOrEmpty(obj, message, args);
        }

        public static void ShouldNotBeNullOrEmpty(this string obj, string message = null, params object[] args)
        {
            Assert.IsNotNullOrEmpty(obj, message, args);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
        }

        public static void ShouldBeGreaterThan(this object actual, object expected)
        {
            Assert.That(actual, Is.GreaterThan(expected));
        }

        public static void ShouldBeLessThan(this object actual, object expected)
        {
            Assert.That(actual, Is.LessThan(expected));
        }

        public static void ShouldContainerInOrder<T>(this IEnumerable<T> actual, params T[] expected)
        {
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
