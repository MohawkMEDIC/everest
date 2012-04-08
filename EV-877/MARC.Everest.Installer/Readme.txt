MARC.Everest.Installer Readme
-----------------------------

[INNO SETUP REQUIREMENTS]

The MARC-HI Everest Framework installation project uses the Inno Setup Compiler (ISCC) to compile the installation
application for the MARC-HI Everest Framework. This compile occurs only in "Release" mode. In order to produce the
installation file, the following applications MUST be installed on your system:

	- Inno Setup Compiler 5.3.0 or better (in C:\Program Files\Inno Setup 5)
	- Inno Setup Pre-Processor library
	
When in release mode, the compile will result in the file "everest.exe" being placed in the $dir\bin\release 
folder.

[INSTALL SUPPORT FILES]

The "installsupp" directory contains a variety of files that provide value add to the installation program. These
include:

	- A compiled copy of MR2009 2.04.1 class library
	- A copy of the MR2009 (2.04.1) MIFs
	- A PDF copy of the developer's guide
	- A CHM of the MARC-HI Everest Framework code documentation
	- The Inno Setup Download Component (used for auto-detecting and installing .NET 3.5 on install)
	
These files may be licensed under different terms than the MARC-HI Everest project. 