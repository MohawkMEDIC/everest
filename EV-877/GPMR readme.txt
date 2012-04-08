General Purpose MIF Rendering Tool (GPMR)
=========================================


1. Introduction

The general purpose MIF rendering tool (GPMR) is a console application that 
has the ability to render HL7v3 Model Information Format (MIF) files into 
any format for which it has a renderer. 

The tool uses a pipeline methodology to perform the rendering and has four 
major stages:

	1. Read MIF 2.x files into the pipeline data segment
	2. Compile the MIF files into the Common Object Representation (COR)
	     format.
	3. Render the COR format repository in memory to whatever format(s)
	     the user desires.
	4. Apply any post-processing steps to complete the rendering (ie:
	     if there is any conversion or formatting assemblies to be created)

Depending on the renderer, stage 3 and 4 of the pipeline may be executed
as a single stage (Exmaple: DEKI renderer has an HTML and Publish as one
stage).


1.1. Notes about MIF 1.x files

Please note that compiling COR objects from MIF 1.0 files is only partially 
developed. While much of the infrastructure works for the compilation of MIF 
1.0 files, there are many outstanding issues that may not be addressed in 
any future releases. This is due, primarily, to the fact that Canada Health
Infoway has deemed MIF 1.0 structures obsolete.


1.2. Notes about the COR Format

Some people may note that the COR structured data has less information than 
the MIF structures. This is an intentional design feature to allow for easier 
processing of the files "down the pipeline". By eliminating features present 
in the MIF that do not relate to the structure and documentation surrounding
the model, we have made it easier to write rendering components.

COR contains only the minimal subset of data required to generate C#, Java, 
HTML etc... documentation and structures.


2. Usage

As stated above, GPMR is a console utility. As such, it accepts a wide variety
of command line arguments that will modify the behavior of the rendering 
process. Command line arguments are broken into two distinct categories

	1. GPMR Parameters: Control the processing of the pipeline
	2. Rendering Parameters: Control an individual renderer

Parameters may be passed in one of two ways (GPMR uses UNIX style parameters)

	1. Shorthand: Many parameters support short hand notation. Parameters 
		passed using method are in the format -{argument} {value} 
		for example:
		
		-v 9		=	 Set verbosity level to 9
	
		You may also combine arguments together so long as the argument
		is a switch (non value), for example:

		-dr DEKI 	=	Set verbosity to debug and use DEKI 
					renderer	

	2. Longhand: Many parameters support a long hand notation. Parameters
		passed using this method are in the format --{arg}={value}, for
		example:
	
		--verbosity=9	=	Set verbosity level to 9

		Unlike shorthand parameters you can't combine parameters in a
		single string, you have to separate them
	
		--debug --renderer=DEKI


2.1. GPMR Parameters

The following are parameters supported by GPMR

	Output Control
	
		-v | --verbosity	Controls the verbosity of output
						1 - Fatal
						2 - Errors
						4 - Warnings
						8 - Debug
						16 - Information
		-d | --debug		Output debug/warning/error
					and fatal messages
		-e | --errors		Output error and fatal messages
		-c | --chatty		Output all messages
		-q | --quiet		Output no messages

	Help
	
		--version		Show version information and quit
		-? | --help		Show help and exit
	
	Input Control

		-s | --source		Add source MIF file (or wildcard)
					(this is also the default parm)
		
	Output Control
	
		-o | --output		Set the target directory
		--extension		Add an extension to the pipeline
		-r | --renderer		Add a renderer

2.2. Pipeline Debug

		--pipe-sniffer		Dumps the contents of the pipeline
					data segment when the pipeline changes
					states. To a file or stdout

2.3. DEKI / HTML Rendering

		--deki-url		The URL of the Mindtouch DEKI-WIKI
					server to publish to
		--deki-path		The path on the server to publish
					documents
		--deki-user		The name of the user on the DEKI 
					server to publish as
		--deki-password		The password of the user (will be
					prompted for a password if not
					supplied)
		--deki-htmlpath		A local path to save HTML files to
		--deki-template		The name of the HTML template
					to use (not supported yet)
		--deki-nopub		Don't perform a publish, just render
					HTML files to deki-htmlpath

2.4. RIMBAPI C# Renderer

		--rimbapi-root-class	Name of the root class to use for 
					inheritence structure (default is 
					RIM.InfrastructureRoot)
		--rimbapi-target-ns	Set the target namespace for generated
					files (default is "output")
		--rimbapi-compile	Compile the resultant C# project
		--rimbapi-dllonly	Only save the DLL, PDB and XML files
					from compile, discard everything else
		--rimbapi-license	The license that should be appended to
					the gerenated files (BSD or MIT are
					currently supported)
		--rimbapi-org		The name of the organization to place
					in the generated files (example:
					Mohawk College of Applied Arts and Ty)
		--rimbapi-gen-vocab	If true, will generate all vocabulary
					as enumerations in the C# files. If false
					only structural element's vocabulary 
					domains will be generated. Default=false
		--rimbapi-gen-rim	If true, will generate classes for the 
					entire RIM namespace. Note: The 
					generated project may not compile with
					this flag turned on! Default = false


2.5. Example uses

Generate DEKI WIKI documentation and publish to server 192.168.0.100 at location
/MyMifDocumentation under user fyfej

	gpmr -r DEKI "C:\mifs\*.*mif" --deki-url=http://192.168.0.100 
		--deki-path=/MyMifDocumentation --deki-user=fyfej

Generate HTML documentation and place into C:\html 

	gpmr -r DEKI "C:\mifs\*.*mif" --deki-nopub=true --deki-htmlpath=C:\html

Generate Visual Studio project in C:\cs

	gpmr -r RIMBA_CS "C:\mifs\*.*mif" -o "C:\cs"

Generate Visual Studio project in C:\CS with namespace "test.api", organization
"My Organization", with BSD license, all vocabulary, all RIM classes

	gpmr -r RIMBA_CS "C:\mifs\*.*mif" -o "C:\cs" --rimbapi-target-ns=test.api
		--rimbapi-gen-rim=true --rimbapi-gen-vocab=true 
		--rimbapi-license=bsd --rimbapi-org="My Organization"

Generate a DLL in C:\CS with namespace test.api and all vocabulary

	gpmr -r RIMBA_CS "C:\mifs\*.*mif" -o "C:\cs" --rimbapi-target-ns=test.api
		--rimbapi-gen-vocab=true --rimbapi-license=bsd 
		--rimbapi-org="My Organization"
		--rimbapi-compile=true --rimbapi-dllonly=true

	
		
		