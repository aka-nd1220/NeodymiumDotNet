#!/usr/bin/env python3
import numpy.random as npr
import numpy.linalg as npl
import pytest


A = npr.rand(1000, 1000)


def det() -> float:
    return npl.det(A)


def test_det(benchmark):
    benchmark.pedantic(det, rounds=256, iterations=64)


if __name__ == "__main__":
    pytest.main(['-v', __file__])
