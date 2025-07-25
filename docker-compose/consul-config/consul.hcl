datacenter = "dc1"
data_dir = "/consul/data"
server = true
bootstrap_expect = 1
ui = true
bind_addr = "0.0.0.0"
client_addr = "0.0.0.0"

encrypt = "RUtypQ8jM8CSwFdlbxr9K0Ou8ekX0Ex5I7cEFjCG+n0="

acl {
  enabled = true
  default_policy = "deny"
  tokens {
    master = "pTscrNjLaGLlZpdTXVTjCu3n5/DjCLi46SGroE+ulFw="
  }
}
