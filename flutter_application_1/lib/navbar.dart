import 'package:flutter/material.dart';
import 'package:flutter_application_1/laporan.dart';
import 'package:flutter_application_1/home.dart';
import 'package:flutter_application_1/login.dart';
import 'package:flutter_application_1/pegawai.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class NavBar extends StatefulWidget {
  const NavBar({Key? key}) : super(key: key);

  @override
  _NavBarState createState() => _NavBarState();
}

class _NavBarState extends State<NavBar> {
  String _employeeName = 'Loading...'; // Default text saat loading

  @override
  void initState() {
    super.initState();
    _getEmployeeName(); // Panggil method untuk mengambil nama pegawai saat inisialisasi widget
  }

  Future<void> _getEmployeeName() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? employeeName = prefs.getString('employeeName');
    setState(() {
      _employeeName = employeeName ?? 'Employee Name'; // Default jika kosong
    });
  }

  Future<void> _logout() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? employeeId = prefs.getString('employeeId');
    if (employeeId != null) {
      // Ganti URL dengan endpoint logout di server Golang Anda
      var url = Uri.parse('https://praktikum-cpanel-unbin.com/golang_api/delimart_api/logout/$employeeId');
      var response = await http.post(url);

      if (response.statusCode == 200) {
        // Clear local storage (SharedPreferences)
        await prefs.clear();
        // Navigate to login screen
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (context) => Login()),
          (route) =>
              false, // Prevents user from going back to the previous screen
        );
      } else {
        // Handle logout failure (show error message or retry)
        showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: Text('Logout Failed'),
            content: Text('Failed to logout. Please try again.'),
            actions: <Widget>[
              TextButton(
                onPressed: () => Navigator.of(context).pop(),
                child: Text('OK'),
              ),
            ],
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          UserAccountsDrawerHeader(
            accountName: FutureBuilder(
              future: _getEmployeeName(),
              builder: (context, snapshot) {
                return Text(
                  _employeeName,
                  style: TextStyle(fontWeight: FontWeight.bold),
                );
              },
            ),
            accountEmail: const Text(
              'Kepala Toko',
            ),
            currentAccountPicture: CircleAvatar(
              child: ClipOval(
                child: Image.asset(
                  'images/Logo_Shape.png',
                  width: 100,
                  height: 100,
                  fit: BoxFit.cover,
                ),
              ),
            ),
            decoration: const BoxDecoration(
              color: Color.fromARGB(255, 2, 10, 74),
              image: DecorationImage(
                image: AssetImage('images/back.jpg'),
                fit: BoxFit.cover,
                colorFilter: ColorFilter.mode(
                  Colors.black54,
                  BlendMode.darken,
                ),
              ),
            ),
          ),
          ListTile(
            leading: const Icon(Icons.home, size: 30, color: Colors.white),
            title: const Text(
              'Home',
              style: TextStyle(
                  color: Colors.white,
                  fontWeight: FontWeight.bold,
                  fontSize: 20),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => const HomePage()),
              );
            },
          ),
          ListTile(
            leading:
                const Icon(Icons.receipt_long, size: 30, color: Colors.white),
            title: const Text(
              'Laporan',
              style: TextStyle(
                  color: Colors.white,
                  fontWeight: FontWeight.bold,
                  fontSize: 20),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => LaporanPage()),
              );
            },
          ),
          ListTile(
            leading: const Icon(Icons.person, size: 30, color: Colors.white),
            title: const Text(
              'Pegawai',
              style: TextStyle(
                  color: Colors.white,
                  fontWeight: FontWeight.bold,
                  fontSize: 20),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => PegawaiPage()),
              );
            },
          ),
          const Divider(color: Colors.white),
          ListTile(
            leading: const Icon(Icons.logout, size: 30, color: Colors.white),
            title: const Text(
              'Log Out',
              style: TextStyle(
                  color: Colors.white,
                  fontWeight: FontWeight.bold,
                  fontSize: 20),
            ),
            onTap: _logout, // Panggil fungsi logout di sini
          ),
        ],
      ),
      backgroundColor: const Color.fromARGB(255, 2, 10, 74),
    );
  }
}
