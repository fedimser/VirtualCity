# VirtualCity

This Windows application allows you to create a model of a city and observe car traffic in it in real time.

## Model

The city consists of Intersections, Roads, Buildings and Cars. Buildings are rectangles. Intersections are points. Roads are lines connecting Intersections. Car is a small rectangle which is moving along roads.

Every Car leaves one Building and aims to arrive to another Building. It can move only along the Roads. 

Every Road can have one or more lanes. On each lane cars can move only in one direction. Cars can change lanes.

Cars move in such a wy that they avoid collisions. If there is a car ahead, car which is behind will maintain safe distance. If there is free lane, car may attempt overtaking.

Intersections can be unregulated or regulated (with traffic lights). On unregulated intersections car which came first goes first. On regulated intersection there is a schedule defined by user, encoding in which time itervals which transitions between roads are allowed. This schedule is repeated in a loop.

Every car calculates its route using Dijkstra's algorithm. However, it not just takes into account length of roads, but for every Road estimates time it will take to go through it, which is based on load of that road. In other words, they can detect and avoid traffic jams.

## System requirements

This app requires Windows 7 or later and .NET framework 4.0.

## Downloads

* [Version 1.0](http://fedimser.github.io/download/virtualcity/VirtualCity_1.0.rar)
* [Version 1.2 (latest)](http://fedimser.github.io/download/virtualcity/VirtualCity_1.2.rar)
* [Examples](http://fedimser.github.io/download/virtualcity/VirtualCity_Examples.rar)

UI language is Ukraininan.

## Historical notes

This app is part of my graduation project at [high school](https://www.dlit.dp.ua/). It was made over course of my last year there (2012-2013). I didn't make any changes to it snce. I uploaded source code to GitHub in 2017. This description and MIT license were added in 2019. 

## Documentation

I prepared two versions of documentation for this project (both in Ukrainian):

* Report which I submitted to comission when I was presenting my project ([pdf](https://fedimser.github.io/download/articles/Fedoriaka2013CityModellingReport.pdf));
* Journal-style paper ([pdf](https://fedimser.github.io/download/articles/CityModelling2013.pdf)).

## Acknowledgements

I am very greatful to my teacher and supervisor on this project, Joseph Entin, and author of the idiea and advisor, Sergey Khenkin. Thanks to them I learned a lot about software engineering while working  on this project.

Logo created by Daniel Omelchenko.
