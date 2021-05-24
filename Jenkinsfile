pipeline {
    agent any
    stages {
        stage('build') {
            steps {
                bat 'dotnet publish -c Release'
            }
            post {
                success {
                    script {
                        bat 'powershell compress-archive bin/Release/netcoreapp3.1/publish/* FirstFunc.zip -Update'
                        script {
                            archiveArtifacts artifacts: 'FirstFunc.zip', fingerprint: true
                        }
                    }
                }
            }
        }
        stage('Deploy to Artifactory') {
            steps {
                script {
                    def server = Artifactory.server 'ssa90'

                    def uploadSpec ="""{
                        "files": [{
                            "pattern": "FirstFunc.zip",
                            "target": "example-repo-local"
                        }]
                    }"""

                    server.upload(uploadSpec)
                }
            }
        }
    }
}
