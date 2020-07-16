using System;
using System.Threading;
using NUnit.Framework;
using MyWaitAll;

namespace Tests
{
    public class WaitAllShould
    {
        [Test]
        public void Should_throw_exception_when_trying_to_set_atom_with_id_grater_then_atoms_count()
        {
            WaitAll waitAll = new WaitAll(10);
            Assert.Throws<ArgumentException>(() => waitAll.SetAtomSignaled(11));
            Assert.Throws<ArgumentException>(() => waitAll.SetAtomNoSignaled(11));
        }

        [Test]
        public void Should_release_when_all_atoms_signaled()
        {
            WaitAll waitAll = new WaitAll(2);
            Thread thread = new Thread(() =>
            {
                waitAll.Wait();
                Assert.True(true);
            });

            thread.Start();

            waitAll.SetAtomSignaled(0);
            waitAll.SetAtomSignaled(1);

            thread.Join();
        }

        [Test]
        public void Should_release_all_when_all_atoms_signaled()
        {
            WaitAll waitAll = new WaitAll(2);
            Thread thread1 = new Thread(() =>
            {
                waitAll.Wait();
                Assert.True(true);
            });
            Thread thread2 = new Thread(() =>
            {
                waitAll.Wait();
                Assert.True(true);
            });

            thread1.Start();
            thread2.Start();

            waitAll.SetAtomSignaled(0);
            waitAll.SetAtomSignaled(1);

            thread1.Join();
            thread2.Join();
        }

        [Test]
        public void Should_not_release_when_not_all_signaled()
        {
            WaitAll waitAll = new WaitAll(2);
            waitAll.SetAtomSignaled(0);
            Assert.False(waitAll.Wait(100));
        }

    }
}