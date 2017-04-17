dotnet publish --framework netcoreapp1.0 --runtime ubuntu.14.04-x64 -c release
sudo scp -r bin/release/netcoreapp1.0/publish/. root@78.24.223.121:~/www/auth.leadder.ru

