Mohawk College GPMR COR Compiler Service
========================================

This class library contains all the neccessary functions to process (or compile) raw MIF 1.0 or 2.0
data structures into the Common object Representation (COR) used for later processing in the GPMR
pipeline stage. 

Folder Structure:

CommonRepresentation - Contains the class definitions for the COR data format
Mif10/Mif20		- Major elements related to the MIF 1.0 / 2.0 data respectively
Compilers		- Contains the controller classes that dictate how MIF format structures should
				  be compiled into the COR format
Parsers			- Contains the classes that will parse each major MIF structure and render an 
				  appropriate COR object from the data contained in the MIF structure

Notes:

MIF 1.0 Compilation 

Please note that compiling COR objects from MIF 1.0 files was only partially developed. While much of the 
infrastructure works for the compilation of MIF 1.0 files, there are many outstanding issues that may
not be addressed in any future releases. This is due, primarily, to the fact that Canada Health
Infoway has deemed MIF 1.0 structures obsolete.

COR Data Structures Compared to MIF Structures

Some people may note that the COR structured data has less information than the MIF structures. This
is an intentional design feature to allow for easier processing of the files "down the pipeline". By 
eliminating features present in the MIF that do not relate to the structure and documentation surrounding
the model, we have made it easier to write rendering components.

COR contains only the minimal subset of data required to generate C#, Java, HTML etc... documentation and 
structures.
