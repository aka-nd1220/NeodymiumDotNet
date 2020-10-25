# File format of .ndnet

## General

`*.ndnet` is archive file format for `NeodymiumDotNet.NdArray<T>`.

All primitive types which has 2 or more bytes are ordered as little endian.

If `T` is primitive type or unmanaged struct, each element of contents are natural byte expression on .Net runtime.

If type `T` is class, each element of contents are serialized byte by `DataContractSerializer`, so `T` must have `DataContractAttribute`.

## Magic number

First 16 bytes are magic number.

Always they are `NeodymiumDotNet\0` on ASCII encoding.

## Full qualified type name

The followings of magic number are assembly qualified type name.

First 4 bytes are little endian 32-bit integer `l`.

`l` means byte length of string part of type name.

Next `l` bytes are name string which are encoded as UTF-8.

Name string contains '\0' on tail.

## Shape

The followings of full qualified type name are shape array.

Shape is 32-bit integer array whose first 4 bytes mean rank (=length of shape array).

So, the byte length of shape is `4 * (length + 1)`.

## Contents

The followings of shape are contents.

It is single dimension array of `T` whose first 4 bytes mean element number (=product of shape array).
