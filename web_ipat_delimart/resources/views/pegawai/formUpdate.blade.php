@extends('layouts.app')

@section('title', 'Form Edit Pegawai')

@section('contents')
  <form action="{{ isset($pegawai['kd_pegawai']) ? route('pegawai.tambah.update', $pegawai['kd_pegawai']) : '' }}" method="post">
    @csrf
    <div class="row">
      <div class="col-12">
        <div class="card shadow mb-4">
          <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Form Edit Pegawai</h6>
          </div>
          <div class="card-body">
            @if(isset($pegawai['kd_pegawai']))
              <div class="form-group">
                <input type="hidden" class="form-control" id="kd_pegawai" name="kd_pegawai" value="{{ $pegawai['kd_pegawai'] }}" readonly>
              </div>
              <div class="form-group">
                <label for="nama_pegawai">Nama Pegawai</label>
                <input type="text" class="form-control" id="nama_pegawai" name="nama_pegawai" value="{{ $pegawai['nama_pegawai'] }}">
              </div>
              <div class="form-group">
                <label for="tanggal_lahir">Tanggal Lahir</label>
                <input type="date" class="form-control" id="tanggal_lahir" name="tanggal_lahir" value="{{ $pegawai['tanggal_lahir'] }}">
              </div>
              <div class="form-group">
                <label for="jenis_kelamin">Jenis Kelamin</label>
                <div>
                  <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" name="jenis_kelamin" id="jenis_kelamin_pria" value="Pria" {{ $pegawai['jenis_kelamin'] == 'Pria' ? 'checked' : '' }}>
                    <label class="form-check-label" for="jenis_kelamin_pria">Pria</label>
                  </div>
                  <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" name="jenis_kelamin" id="jenis_kelamin_wanita" value="Wanita" {{ $pegawai['jenis_kelamin'] == 'Wanita' ? 'checked' : '' }}>
                    <label class="form-check-label" for="jenis_kelamin_wanita">Perempuan</label>
                  </div>
                </div>
              </div>
              <div class="form-group">
                <label for="alamat">Alamat</label>
                <input type="text" class="form-control" id="alamat" name="alamat" value="{{ $pegawai['alamat'] }}">
              </div>
              <div class="form-group">
                <label for="telepon">Telepon</label>
                <input type="text" class="form-control" id="telepon" name="telepon" value="{{ $pegawai['telepon'] }}">
              </div>
              <div class="form-group">
                <label for="kd_role">Role</label>
                <select name="kd_role" id="kd_role" class="custom-select">
                  <option value="" selected disabled hidden>-- Pilih Role --</option>
                  @foreach ($role as $row)
                    <option value="{{ $row['kd_role'] }}" {{ $pegawai['kd_role'] == $row['kd_role'] ? 'selected' : '' }}>{{ $row['role'] }}</option>
                  @endforeach
                </select>
              </div>
            @else
              <div class="alert alert-danger">Data pegawai tidak ditemukan.</div>
            @endif
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-primary {{ isset($pegawai['kd_pegawai']) ? '' : 'disabled' }}">Update</button>
            <a href="{{ route('pegawai') }}" class="btn btn-secondary">Batal</a>
          </div>
        </div>
      </div>
    </div>
  </form>
@endsection
