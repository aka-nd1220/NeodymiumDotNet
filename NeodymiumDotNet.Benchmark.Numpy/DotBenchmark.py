#!/usr/bin/env python3
import numpy as np
import numpy.random as npr
import pytest


size = 256
A = npr.rand(size, size).astype(np.float64)
B = npr.rand(size, size).astype(np.float64)


def dot():
    return np.dot(A, B)


def test_dot(benchmark):
    benchmark.pedantic(dot, rounds=256, iterations=16)


if __name__ == "__main__":
    pytest.main(['-v', __file__])
