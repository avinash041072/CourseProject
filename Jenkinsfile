pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    bat "dotnet restore"
                    bat "dotnet build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    bat "dotnet test --no-restore --configuration Release"
                }
            }
        } // ✅ This closing brace was missing!

        stage('Publish') {
            steps {
                script {
                    bat "dotnet publish --no-restore --configuration Release --output .\\publish"
                }
                archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
            }
        }

        stage('Deploy to Remote Server') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'remoteuser', usernameVariable: 'USER', passwordVariable: 'PASS')]) {
                    script {
                        def publishDir = "${WORKSPACE}\\publish"
                        def remotePath = "C:\\\\WebsiteContent\\\\Courseapp"  // escaped backslashes

                        bat """
                        powershell -Command "Invoke-Command -ComputerName 194.233.83.33 -ScriptBlock {
                            param([string]`$path)
                            if (!(Test-Path `$path)) { New-Item -ItemType Directory -Path `$path }
                        } -ArgumentList '${remotePath}' -Credential (New-Object System.Management.Automation.PSCredential('${USER}', (ConvertTo-SecureString '${PASS}' -AsPlainText -Force)))"

                        powershell -Command "Copy-Item -Path '${publishDir}\\*' -Destination '${remotePath}' -Recurse -Force"
                        """
                    }
                }
            }
        }
    }

    post {
        success {
            echo '✅ Build, test, publish, and deploy successful!'
        }
        failure {
            echo '❌ Build failed.'
        }
    }
}
