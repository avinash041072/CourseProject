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
        }

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
        withCredentials([
            usernamePassword(
                credentialsId: 'remoteuser',
                usernameVariable: 'CREDENTIAL_USERNAME',
                passwordVariable: 'CREDENTIAL_PASSWORD'
            )
        ]) {
            powershell '''
                # Create secure credentials from Jenkins environment variables
                $credentials = New-Object System.Management.Automation.PSCredential(
                    $env:CREDENTIAL_USERNAME,
                    (ConvertTo-SecureString $env:CREDENTIAL_PASSWORD -AsPlainText -Force)
                )

                # Map the shared folder to a drive (X:)
                New-PSDrive -Name X -PSProvider FileSystem -Root "\\\\111.233.83.33\\Courseapp" -Persist -Credential $credentials

                # Copy published files to the mapped drive
                Copy-Item -Path ".\\publish\\*" -Destination "X:\\" -Recurse -Force

                # Unmap the drive after copy
                Remove-PSDrive -Name X
            '''
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
