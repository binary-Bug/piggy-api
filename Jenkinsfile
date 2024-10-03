pipeline {
    agent any
    stages {
        stage('Set Build Name'){
            steps {
                script {
                    currentBuild.displayName = "#${params.Branch_Name.split("/")[2].toUpperCase()} - v${params.MAJOR}.${params.MINOR}."+(("${BUILD_NUMBER}" as Integer)-1)
                }
            }
        }
        stage('Verify dotnet sdk version') {
            steps {
                sh 'dotnet --info'
            }
        }
        stage('Verify dotnet runtime version') {
            steps {
                sh 'dotnet --version'
            }
        }
        stage('Restore Packages') {
            steps {
                sh 'dotnet restore "./Piggy.Api.csproj"'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build "./Piggy.Api.csproj" -c Release'
            }
        }
        stage('Clear Workspace') {
            steps {
                script{
                    pwd = pwd()
                }
                echo "CLEARING $pwd"
                sh 'rm -r *'
                echo "$pwd CLEANED"
                dir('../') {
                    script{
                    pwd = pwd()
                     echo "CLEARING $pwd"
                    sh 'rm -r *'
                    echo "$pwd CLEANED"
                    }   
                }
            }
        }
        stage('Return Job Status to Github') {
            steps {
                sh 'echo Completed'
            }
        }
    }
}