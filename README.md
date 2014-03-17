Option pricing using PDEs in .Net
=================================

.Net library written in C# for option pricing using PDEs (Partial Differential Equations). The library is written using .Net Framework version 4.5. Currently the library implements three Finite Difference methods for the calculation of the option price surface:

- explicit,
- implicit,
- and Crank-Nicolson.

I hope to further extend the library with other types of methods in the future.

Usage
=====

Folder `Examples` under `src` contains an example of how to calculate the option price surface of an European strangle spread option using the implicit method. The result can be written to a text file and plotted in Matlab. Folder `matlab` contains a sample script to plot the result of the above mentioned example. The result looks like this: 

![Alt text](/matlab/EuropeanStrangleSpread.png "European strangle spread option surface")
