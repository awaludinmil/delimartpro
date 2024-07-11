@extends('layouts.app')

@section('title', 'Form Tambah Pegawai')

@section('contents')
  <form action="{{ route('pegawai.tambah.simpan') }}" method="post">
    @csrf
    <div class="row">
      <div class="col-12">
        <div class="card shadow mb-4">
          <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Form Tambah Pegawai</h6>
          </div>
          <div class="card-body">

            <div class="form-group">
              <label for="nama_pegawai">Nama Pegawai</label>
              <input type="text" class="form-control" id="nama_pegawai" name="nama_pegawai">
            </div>
            <div class="form-group">
              <label for="tanggal_lahir">Tanggal Lahir</label>
              <input type="date" class="form-control" id="tanggal_lahir" name="tanggal_lahir">
            </div>
            <div class="form-group">
              <label for="jenis_kelamin">Jenis Kelamin</label>
              <div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="jenis_kelamin" id="jenis_kelamin_pria" value="Pria">
                  <label class="form-check-label" for="jenis_kelamin_pria">Pria</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="jenis_kelamin" id="jenis_kelamin_wanita" value="Wanita">
                  <label class="form-check-label" for="jenis_kelamin_wanita">Wanita</label>
                </div>
              </div>
            </div>
            <div class="form-group">
              <label for="alamat">Alamat</label>
              <input type="text" class="form-control" id="alamat" name="alamat">
            </div>
            <div class="form-group">
              <label for="telepon">Telepon</label>
              <input type="text" class="form-control" id="telepon" name="telepon">
            </div>
            <div class="form-group">
              <label for="kd_role">Role</label>
              <select name="kd_role" id="kd_role" class="custom-select">
                <option value="" selected disabled hidden>-- Pilih Role --</option>
                @foreach ($role as $row)
                  <option value="{{ $row['kd_role'] }}">{{ $row['role'] }}</option>
                @endforeach
              </select>
            </div>
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-primary">Simpan</button>
            <a href="{{ route('pegawai') }}" class="btn btn-secondary">Batal</a>
          </div>
        </div>
      </div>
    </div>
  </form>
@endsection
