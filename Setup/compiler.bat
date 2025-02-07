@echo off
chcp 65001 > nul
call alter_version

call publish_api

call publish_service

call gen_setup
pause