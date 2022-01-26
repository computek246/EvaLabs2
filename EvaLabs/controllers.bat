@echo off
set "dataContext=EvaLabs.Domain.Context.EvaContext"
set "relativeFolderPath=Controllers"


for /F "tokens=1* delims=<" %%A in (Entities.txt) do (
	for /F "tokens=1* delims=>" %%A in ("%%B") do (
		for /F "tokens=1 delims= " %%R in ("%%B") do (
			dotnet aspnet-codegenerator controller --model %%~A --dataContext %dataContext% --useDefaultLayout --referenceScriptLibraries --relativeFolderPath %relativeFolderPath% --useAsyncActions --controllerName %%~RController --force
		)
	)
)

pause