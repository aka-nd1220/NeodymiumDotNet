#!/usr/bin/env python3
from decimal import Decimal
import numpy as np
import numpy.random as npr
import scipy.linalg as scl
import pytest


size = 256
A = npr.rand(size, size).astype(np.float64)
B = np.vectorize(Decimal)(A)


def dot_primitive():
    return scl.lu(A)


def dot_standard():
    return scl.lu(B)


def test_dot_primitive(benchmark):
    benchmark.pedantic(dot_primitive, rounds=256, iterations=16)


def test_dot_standard(benchmark):
    benchmark.pedantic(dot_standard, rounds=256, iterations=16)


if __name__ == "__main__":
    pytest.main(['-v', __file__])
