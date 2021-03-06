[![Travis build Status](https://travis-ci.org/nodatime/nodatime.serialization.svg?branch=master)](https://travis-ci.org/nodatime/nodatime.serialization)

# Noda Time serialization

This repository contains projects to do with serializing [Noda Time](http://nodatime.org)
data in various forms, such as JSON.

.NET binary serialization and XML serialization is built into Noda Time itself, but
other serialization mechanisms are expected to simply use the public API.

Separating the code into a "core" in the [nodatime](https://github.com/nodatime/nodatime)
repository and serialization code here allows for a greater separation between the two,
and independent release cycles.
