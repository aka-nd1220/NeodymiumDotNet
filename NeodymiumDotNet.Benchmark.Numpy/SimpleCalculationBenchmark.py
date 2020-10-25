#!/usr/bin/env python3
import numpy as np
import numpy.random as npr
import pytest


A1    = npr.rand(   1,    1)
B1    = npr.rand(   1,    1)
C1    = npr.rand(   1,    1)
A3    = npr.rand(   3,    3)
B3    = npr.rand(   3,    3)
C3    = npr.rand(   3,    3)
A10   = npr.rand(  10,   10)
B10   = npr.rand(  10,   10)
C10   = npr.rand(  10,   10)
A30   = npr.rand(  30,   30)
B30   = npr.rand(  30,   30)
C30   = npr.rand(  30,   30)
A100  = npr.rand( 100,  100)
B100  = npr.rand( 100,  100)
C100  = npr.rand( 100,  100)
A300  = npr.rand( 300,  300)
B300  = npr.rand( 300,  300)
C300  = npr.rand( 300,  300)
A1000 = npr.rand(1000, 1000)
B1000 = npr.rand(1000, 1000)
C1000 = npr.rand(1000, 1000)
A3000 = npr.rand(3000, 3000)
B3000 = npr.rand(3000, 3000)
C3000 = npr.rand(3000, 3000)

NC_A1    = list(A1   .flatten())
NC_B1    = list(B1   .flatten())
NC_C1    = list(C1   .flatten())
NC_A3    = list(A3   .flatten())
NC_B3    = list(B3   .flatten())
NC_C3    = list(C3   .flatten())
NC_A10   = list(A10  .flatten())
NC_B10   = list(B10  .flatten())
NC_C10   = list(C10  .flatten())
NC_A30   = list(A30  .flatten())
NC_B30   = list(B30  .flatten())
NC_C30   = list(C30  .flatten())
NC_A100  = list(A100 .flatten())
NC_B100  = list(B100 .flatten())
NC_C100  = list(C100 .flatten())
NC_A300  = list(A300 .flatten())
NC_B300  = list(B300 .flatten())
NC_C300  = list(C300 .flatten())
NC_A1000 = list(A1000.flatten())
NC_B1000 = list(B1000.flatten())
NC_C1000 = list(C1000.flatten())
NC_A3000 = list(A3000.flatten())
NC_B3000 = list(B3000.flatten())
NC_C3000 = list(C3000.flatten())


def add_numpy_core(a: np.ndarray, b: np.ndarray, c: np.ndarray) -> np.ndarray:
    return a + b + c


def add_simple_core(a: list, b: list, c: list) -> list:
    retval = [0.0] * len(a)
    for i in range(len(a)):
        retval[i] = a[i] + b[i] + c[i]
    return retval


def add_numpy_1   (): return add_numpy_core(A1   , B1   , C1   )
def add_numpy_3   (): return add_numpy_core(A3   , B3   , C3   )
def add_numpy_10  (): return add_numpy_core(A10  , B10  , C10  )
def add_numpy_30  (): return add_numpy_core(A30  , B30  , C30  )
def add_numpy_100 (): return add_numpy_core(A100 , B100 , C100 )
def add_numpy_300 (): return add_numpy_core(A300 , B300 , C300 )
def add_numpy_1000(): return add_numpy_core(A1000, B1000, C1000)
def add_numpy_3000(): return add_numpy_core(A3000, B3000, C3000)
def add_simple_1   (): return add_simple_core(A1   , B1   , C1   )
def add_simple_3   (): return add_simple_core(A3   , B3   , C3   )
def add_simple_10  (): return add_simple_core(A10  , B10  , C10  )
def add_simple_30  (): return add_simple_core(A30  , B30  , C30  )
def add_simple_100 (): return add_simple_core(A100 , B100 , C100 )
def add_simple_300 (): return add_simple_core(A300 , B300 , C300 )
def add_simple_1000(): return add_simple_core(A1000, B1000, C1000)
def add_simple_3000(): return add_simple_core(A3000, B3000, C3000)


def test_add_numpy_1    (benchmark): benchmark.pedantic(add_numpy_1    , rounds=256, iterations=16)
def test_add_numpy_3    (benchmark): benchmark.pedantic(add_numpy_3    , rounds=256, iterations=16)
def test_add_numpy_10   (benchmark): benchmark.pedantic(add_numpy_10   , rounds=256, iterations=16)
def test_add_numpy_30   (benchmark): benchmark.pedantic(add_numpy_30   , rounds=256, iterations=16)
def test_add_numpy_100  (benchmark): benchmark.pedantic(add_numpy_100  , rounds=256, iterations=16)
def test_add_numpy_300  (benchmark): benchmark.pedantic(add_numpy_300  , rounds=256, iterations=16)
def test_add_numpy_1000 (benchmark): benchmark.pedantic(add_numpy_1000 , rounds=256, iterations=16)
def test_add_numpy_3000 (benchmark): benchmark.pedantic(add_numpy_3000 , rounds=256, iterations=16)
def test_add_simple_1   (benchmark): benchmark.pedantic(add_simple_1   , rounds=256, iterations=16)
def test_add_simple_3   (benchmark): benchmark.pedantic(add_simple_3   , rounds=256, iterations=16)
def test_add_simple_10  (benchmark): benchmark.pedantic(add_simple_10  , rounds=256, iterations=16)
def test_add_simple_30  (benchmark): benchmark.pedantic(add_simple_30  , rounds=256, iterations=16)
def test_add_simple_100 (benchmark): benchmark.pedantic(add_simple_100 , rounds=256, iterations=16)
def test_add_simple_300 (benchmark): benchmark.pedantic(add_simple_300 , rounds=256, iterations=16)
def test_add_simple_1000(benchmark): benchmark.pedantic(add_simple_1000, rounds=256, iterations=16)
def test_add_simple_3000(benchmark): benchmark.pedantic(add_simple_3000, rounds=256, iterations=16)


if __name__ == "__main__":
    pytest.main(['-v', __file__])
